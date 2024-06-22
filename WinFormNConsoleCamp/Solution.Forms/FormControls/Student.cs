
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace FormControls
{
    public class Student
    {
        [DisplayName("학번")]
        public int Id { get; set; }


        [DisplayName("이름")]
        public string Name { get; set; }


        [DisplayName("성적")]
        [Range(0, 100, ErrorMessage = "0과 100 사이 점수를 입력하세요.")]
        public int Score { get; set; }

        /// <summary>
        /// 필드는 바인딩 되지 않음
        /// </summary>
        public string Memo;

        public Student(int id, string name, int score)
        {
            Id = id;
            Name = name;
            Score = score;

            // 바인딩 되지 않음
            Memo = $"필드 메모_{Id}";
        }
    }
}
