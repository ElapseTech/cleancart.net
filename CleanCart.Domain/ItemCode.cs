using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCart.Domain
{
    public sealed class ItemCode
    {
        private readonly String _codeValue;

        public ItemCode(String codeValue)
        {
            _codeValue = codeValue;
        } 

        public String CodeValue
        {
            get { return _codeValue; }
        }
    }
}
