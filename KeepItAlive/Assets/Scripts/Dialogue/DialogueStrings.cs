namespace Constants
{
    public class Dialogue
    {
        public enum Character { One, Two }

        public enum Category { Greeting, Beer, Music, MusicTaste, Misc };

        public static Category GetNext(Category category)
        {
            if(category == Category.Greeting)
            {
                return Category.Beer;
            }
            else if(category == Category.Beer)
            {
                return Category.Music;
            }
            else if(category == Category.Music)
            {
                return Category.MusicTaste;
            }
            else if(category == Category.MusicTaste)
            {
                return Category.Misc;
            }
            else
            {
                return Category.Greeting;
            }
        }

        public enum Type { Greeting, Misc, HasBeer, NeedsBeer, NoMusicPlaying, MusicPlaying, LikesMusic, DislikesMusic }

        public static string GetStringByCharacter(Character character, Type type)
        {
            string result = "";

            if (character == Character.One)
            {
                if (type == Type.Greeting)
                {
                    result = "what's up dog?";
                }
                else if (type == Type.Misc)
                {
                    result = "'joker' should have won an oscar";
                }
                else if (type == Type.HasBeer)
                {
                    result = "what is this, 'beer'?";
                }
                else if (type == Type.NeedsBeer)
                {
                    result = "i could really use a beverage!";
                }
                else if (type == Type.NoMusicPlaying)
                {
                    result = "it's pretty quiet here...";
                }
                else if (type == Type.MusicPlaying)
                {
                    result = "finally, some music!";
                }
                else if(type == Type.LikesMusic)
                {
                    result = "good song choice brah";
                }
                else if(type == Type.DislikesMusic)
                {
                    result = "this song is pretty 'mid'";
                }
            }
            else if(character == Character.Two)
            {
                if (type == Type.Greeting)
                {
                    result = "so, who invited you?";
                }
                else if (type == Type.Misc)
                {
                    result = "these glasses are one of a kind, so you know";
                }
                else if (type == Type.HasBeer)
                {
                    result = "hmm..i'm sensing light notes of 'beer'";
                }
                else if (type == Type.NeedsBeer)
                {
                    result = "i require a spirit to imbibe..";
                }
                else if (type == Type.NoMusicPlaying)
                {
                    result = "this shindig could use a jocular tune";
                }
                else if (type == Type.MusicPlaying)
                {
                    result = "ah, the music warms my cockrels";
                }
                else if (type == Type.LikesMusic)
                {
                    result = "this song is rather garish, but i approve";
                }
                else if (type == Type.DislikesMusic)
                {
                    result = "didn't pitchfork give this track a 3/10? amateur pick";
                }
            }

            return result.ToLower().Trim();
        }
    }
}
