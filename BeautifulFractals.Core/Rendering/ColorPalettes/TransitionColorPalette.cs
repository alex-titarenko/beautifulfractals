using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace TAlex.BeautifulFractals.Rendering.ColorPalettes
{
    public class TransitionColorPalette : ColorPalette
    {
        #region Fields

        private TransitionCollection _transitions = new TransitionCollection();

        #endregion

        #region Properties

        public TransitionCollection Transitions
        {
            get
            {
                return _transitions;
            }

            set
            {
                _transitions = value;
            }
        }

        #endregion

        #region Methods

        public override Color GetColor(double value)
        {
            if (_transitions.Count == 0)
            {
                return Color.Empty;
            }

            value = Math.Max(0, Math.Min(1.0, value));

            int currTransition = 0;
            while (currTransition < _transitions.Count && value >= _transitions[currTransition].Position)
            {
                currTransition++;
            }
            if (currTransition > 0) currTransition--;

            Transition transition = _transitions[currTransition];

            if (currTransition >= _transitions.Count - 1)
            {
                return _transitions[currTransition].Color;
            }

            Transition nextTransition = _transitions[currTransition + 1];
            double pos = (value - transition.Position) / (nextTransition.Position - transition.Position);

            byte r = (byte)(transition.Color.R + pos * (nextTransition.Color.R - transition.Color.R));
            byte g = (byte)(transition.Color.G + pos * (nextTransition.Color.G - transition.Color.G));
            byte b = (byte)(transition.Color.B + pos * (nextTransition.Color.B - transition.Color.B));

            return Color.FromArgb(r, g, b);
        }

        #endregion

        #region Nested Type

        public class Transition : IComparable, IComparable<Transition>, INotifyPropertyChanged, IXmlSerializable
        {
            private const string ColorXmlAttrName = "Color";
            private const string PositionXmlAttrName = "Position";

            private Color _color;
            private double _position;

            public Color Color
            {
                get
                {
                    return _color;
                }

                set
                {
                    _color = value;
                    OnNotifyPropertyChanged("Color");
                }
            }

            public double Position
            {
                get
                {
                    return _position;
                }

                set
                {
                    if (value < 0.0 || value > 1.0)
                    {
                        throw new ArgumentOutOfRangeException("Position", "Position must be value from 0 to 1.");
                    }

                    double oldPosition = _position;
                    _position = value;
                    if (_position != oldPosition) OnNotifyPropertyChanged("Position");
                }
            }


            public Transition()
            {
            }

            public Transition(Color color, double position)
            {
                _color = color;
                _position = position;
            }


            public override string ToString()
            {
                return String.Format("Color: {0}, Position: {1}", Color, Position);
            }


            #region IComparable Members

            public int CompareTo(object obj)
            {
                return CompareTo(obj as Transition);
            }

            #endregion

            #region IComparable<Transition> Members

            public int CompareTo(Transition other)
            {
                return Position.CompareTo(other.Position);
            }

            #endregion

            #region IXmlSerializable Members

            public XmlSchema GetSchema()
            {
                throw new NotImplementedException();
            }

            public void ReadXml(XmlReader reader)
            {
                if (reader.MoveToAttribute(ColorXmlAttrName))
                {
                    reader.ReadAttributeValue();
                    Color = Color.Parse(reader.Value);
                }

                if (reader.MoveToAttribute(PositionXmlAttrName))
                {
                    reader.ReadAttributeValue();
                    Position = double.Parse(reader.Value, CultureInfo.InvariantCulture);
                }
            }

            public void WriteXml(XmlWriter writer)
            {
                writer.WriteAttributeString(ColorXmlAttrName, Color.ToString());
                writer.WriteAttributeString(PositionXmlAttrName, Position.ToString(CultureInfo.InvariantCulture));
            }

            #endregion

            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnNotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            #endregion
        }

        public class TransitionCollection : ICollection<Transition>, IList<Transition>, IList
        {
            #region Fields

            private List<Transition> _transitions;

            #endregion

            #region Constructors

            public TransitionCollection()
            {
                _transitions = new List<Transition>();
            }

            #endregion

            #region Methods

            #region IEnumerable<Transition> Members

            public IEnumerator<Transition> GetEnumerator()
            {
                return _transitions.GetEnumerator();
            }

            #endregion

            #region ICollection<Transition> Members

            public void Add(Transition item)
            {
                _transitions.Add(item);
                _transitions.Sort();
                item.PropertyChanged += new PropertyChangedEventHandler(transition_PropertyChanged);
            }

            public void Clear()
            {
                _transitions.Clear();
            }

            public bool Contains(Transition item)
            {
                return _transitions.Contains(item);
            }

            public void CopyTo(Transition[] array, int arrayIndex)
            {
                _transitions.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get
                {
                    return _transitions.Count;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            public bool Remove(Transition item)
            {
                item.PropertyChanged -= new PropertyChangedEventHandler(transition_PropertyChanged);
                return _transitions.Remove(item);
            }

            #endregion

            #region IList<Transition> Members

            public int IndexOf(Transition item)
            {
                return _transitions.IndexOf(item);
            }

            public void Insert(int index, Transition item)
            {
                _transitions.Insert(index, item);
                item.PropertyChanged += new PropertyChangedEventHandler(transition_PropertyChanged);
            }

            public void RemoveAt(int index)
            {
                _transitions.RemoveAt(index);
            }

            public Transition this[int index]
            {
                get
                {
                    return _transitions[index];
                }

                set
                {
                    Add(value);
                }
            }

            #endregion

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _transitions.GetEnumerator();
            }

            #endregion

            #region ICollection Members

            public void CopyTo(Array array, int index)
            {
                ((ICollection)_transitions).CopyTo(array, index);
            }

            public bool IsSynchronized
            {
                get
                {
                    return false;
                }
            }

            public object SyncRoot
            {
                get
                {
                    return this;
                }
            }

            #endregion

            #region IList Members

            public int Add(object value)
            {
                Add((Transition)value);
                return Count - 1;
            }

            public bool Contains(object value)
            {
                if (value is Transition)
                    return Contains((Transition)value);
                else
                    return false;
            }

            public int IndexOf(object value)
            {
                return IndexOf((Transition)value);
            }

            public void Insert(int index, object value)
            {
                Insert(index, (Transition)value);
            }

            public bool IsFixedSize
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public void Remove(object value)
            {
                Remove((Transition)value);
            }

            object IList.this[int index]
            {
                get
                {
                    return _transitions[index];
                }

                set
                {
                    this[index] = value as Transition;
                }
            }

            #endregion

            #region Event Handlers

            private void transition_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Position")
                {
                    _transitions.Sort();
                }
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
