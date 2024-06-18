
namespace FieldValidatorAPI
{
    public class CommonFieldValidatorFunctions
    {
        public delegate bool RequireValidDel(string fieldVal);
        public delegate bool StringLengthValiDel(string fieldVal, int min, int max);
        public delegate bool DateValiDel(string fieldVal, out DateTime validDateTime);
        public delegate bool PatternMatchDel(string fieldVal, string pattern);
        public delegate bool CompareFieldsValidDel(string fieldVAl, string fieldValCompare);
    }
}
