using System.ComponentModel.DataAnnotations.Schema;


namespace QuizTest.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Image {get; set; }
      
        public string QuestionsSerialized { get; set; }
    }
}
