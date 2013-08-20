using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAlex.BeautifulFractals.Infrastructure
{
    public interface ICollectionView : IEnumerable
    {
        object CurrentItem { get; }

        bool IsCurrentAfterLast { get; }
        bool IsCurrentBeforeFirst { get; }

        bool MoveCurrentTo(object item);
        bool MoveCurrentToNext();
        bool MoveCurrentToPrevious();

        void Refresh();

        Predicate<object> Filter { get; set; }
    }
}
