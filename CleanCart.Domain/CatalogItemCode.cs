namespace CleanCart.Domain
{
    public sealed class CatalogItemCode
    {

        private readonly string _codeValue;

        public CatalogItemCode(string codeValue)
        {
            _codeValue = codeValue;
        }

        public string CodeValue
        {
            get { return _codeValue; }
        }

        public override bool Equals(object obj)
        {
            var otherCode = obj as CatalogItemCode;
            return otherCode != null && _codeValue == otherCode.CodeValue;
        }

        public override int GetHashCode()
        {
            return (_codeValue != null ? _codeValue.GetHashCode() : 0);
        }
    }
}
