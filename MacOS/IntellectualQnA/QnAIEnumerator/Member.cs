namespace QnAIEnumerator
{
    public class Member(int id, string name)
    {
        private int Id
        {
            get;
        } = id;


        private string? Name
        {
            get;
        } = name;


        public override string ToString()
        {
            return $"Id = {Id,3},\tName = {Name}";
        }
    }
}