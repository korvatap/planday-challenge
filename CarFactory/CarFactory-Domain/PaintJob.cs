using System;
using System.Drawing;
using System.Text;

namespace CarFactory_Domain
{
    public abstract class PaintJob
    {
        public const string ALLOWED_CHARACTERS = "abcdefghijkmnopqrstuvwxyz0123456789";
        private bool IsUnlocked = false;
        private readonly string Solution;
        public PaintJob()
        {
            Solution = CreateString(PuzzleAnswerLength());
        }
        public (int, long) CreateInstructions()
        {
            return (PuzzleAnswerLength(), EncodeString(Solution));
        }

        public bool TryUnlockInstructions(string answer)
        {
            if (AreInstructionsUnlocked()) throw new Exception("Already unlocked");
            IsUnlocked = EncodeString(answer) == EncodeString(Solution);
            return IsUnlocked;
        }

        public bool AreInstructionsUnlocked() => IsUnlocked;

        protected abstract int PuzzleAnswerLength();

        public static long EncodeString(string text)
        {
            Span<char> result = stackalloc char[text.Length];
            
            const string key = "Planborghini";
            for (var c = 0; c < text.Length; c++)
                result[c] = ((char)(text[c] ^ (uint)key[c % key.Length]));
            return result.ToString().GetHashCode();
        }
   
        private static string CreateString(int stringLength)
        {
            var rd = new Random(((int) DateTime.Now.Ticks)/5*5);
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = ALLOWED_CHARACTERS[rd.Next(0, ALLOWED_CHARACTERS.Length)];
            }

            return new string(chars);
        }
    }

    public class SingleColorPaintJob : PaintJob
    {
        public Color Color { get; set; }
        public SingleColorPaintJob(Color color) : base()
        {
            Color = color;
        }

        protected override int PuzzleAnswerLength() => 2;
    }

    public class StripedPaintJob : PaintJob
    {
        public Color BaseColor { get; set; }
        public Color StripeColor { get; set; }
        public StripedPaintJob(Color baseCol, Color stripeCol) : base()
        {
            BaseColor = baseCol;
            StripeColor = stripeCol;
        }

        protected override int PuzzleAnswerLength() => 4;
    }

    public class DottedPaintJob : PaintJob
    {
        public Color BaseColor { get; set; }
        public Color DotColor { get; set; }
        public DottedPaintJob(Color baseCol, Color dotCol) : base()
        {
            BaseColor = baseCol;
            DotColor = dotCol;
        }

        protected override int PuzzleAnswerLength() => 3;
    }
}
