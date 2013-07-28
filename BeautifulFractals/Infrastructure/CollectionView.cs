using System;
using System.Collections;
using System.Collections.Generic;


namespace TAlex.BeautifulFractals.Infrastructure
{
    public class CollectionView : System.Windows.Data.ListCollectionView, ICollectionView
    {
        public CollectionView(IList collection)
            : base(collection)
        {
        }
    }
}
