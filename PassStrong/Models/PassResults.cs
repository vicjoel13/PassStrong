using Zxcvbn;

namespace PassStrong.Models
{
	public class PassResults
	{

        public string PassWord { get; set; }
        public int Complexity { get; set; }
        public string Time { get; set; }
        public Result CrackResults { get; set; }

    }
}
