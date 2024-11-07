using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Domain.Entitys.Users;

namespace NanoStarCTF.Domain.Entitys.Submissions;

public class Submission
{
    public long UserId { get; set; } // The ID of the user who made the submission
    public long ChallengeId { get; set; } // The ID of the challenge that was submitted
    public string SubmittedFlag { get; set; } // The flag submitted by the user
    public bool IsCorrect { get; set; } // Whether the submission is correct or not
    public DateTime SubmissionTime { get; set; } // The time of submission

    public User User { get; set; } // Navigation property to User
    public Challenge Challenge { get; set; } // Navigation property to Challenge
}