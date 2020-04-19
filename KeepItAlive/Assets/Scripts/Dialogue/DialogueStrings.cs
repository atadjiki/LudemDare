namespace Constants
{
    public class Dialogue
    {
        public enum Character { One, Two }
        public enum Type { Greeting, Misc, HasBeer, NeedsBeer, NoMusicPlaying, MusicPlaying, LikesMusic, DislikesMusic }

        public string GetStringByCharacter(Character character, Type type)
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
                    result = "";
                }
                else if (type == Type.NoMusicPlaying)
                {

                }
                else if (type == Type.MusicPlaying)
                {

                }
                else if(type == Type.LikesMusic)
                {

                }
                else if(type == Type.DislikesMusic)
                {

                }
            }
            else if(character == Character.Two)
            {
                if (type == Type.Greeting)
                {

                }
                else if (type == Type.Misc)
                {

                }
                else if (type == Type.HasBeer)
                {

                }
                else if (type == Type.NeedsBeer)
                {

                }
                else if (type == Type.NoMusicPlaying)
                {

                }
                else if (type == Type.MusicPlaying)
                {

                }
                else if (type == Type.LikesMusic)
                {

                }
                else if (type == Type.DislikesMusic)
                {

                }
            }

            return result.ToLower().Trim();
        }
    }
}
