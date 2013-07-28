using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TAlex.BeautifulFractals.Infrastructure
{
    public class CollectionViewFactory : ICollectionViewFactory
    {
        #region ICollectionViewFactory Members

        public ICollectionView GetView(IEnumerable collection)
        {
            if (collection is IList)
                return new CollectionView((IList)collection);

            throw new ArgumentException("collection");
        }

        #endregion
    }
}
