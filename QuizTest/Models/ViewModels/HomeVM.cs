namespace QuizTest.Models.ViewModels
{
    public class HomeVM
    {
        public Question Question { get; set; }

        public int QuestionCount { get; set; }

        public string Answer { get; set; }

        public int RightAnswersCount { get; set; }
    }
}
