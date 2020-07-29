using System.ComponentModel.DataAnnotations;

namespace classifieds.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}