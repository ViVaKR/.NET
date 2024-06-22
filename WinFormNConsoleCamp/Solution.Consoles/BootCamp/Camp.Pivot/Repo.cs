using System.ComponentModel.DataAnnotations;
namespace Camp.Pivot;

public class Repo
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "이름")]
    public string? Name { get; set; }

    [Display(Name = "시간")]
    public DateTime Runtime { get; set; }

    [Display(Name = "종목")]
    public Item TypeOfItem { get; set; }

    [Display(Name = "횟수")]
    public int NumberOfExec { get; set; }
}

public enum Item
{
    아령 = 0,
    줄넘기,
    고무줄,
    달리기
}
