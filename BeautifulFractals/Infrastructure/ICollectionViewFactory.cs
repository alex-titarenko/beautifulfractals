using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAlex.BeautifulFractals.Infrastructure
{
    public interface ICollectionViewFactory
    {
        ICollectionView GetView(IEnumerable collection);
    }
}
