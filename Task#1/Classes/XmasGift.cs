using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint01.Classes
{
    public class XmasGift : ICollection<CandyItem>
    {
        private ICollection<IMediaItem> mediaItems = new List<IMediaItem>();

        public void Add(CandyItem item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(CandyItem item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(CandyItem[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(CandyItem item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<CandyItem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
