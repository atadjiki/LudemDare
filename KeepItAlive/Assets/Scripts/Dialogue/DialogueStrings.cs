namespace Constants
{
    public class Dialogue
    {
        public enum Character { One, Two, Three, Four, Five, Six, Seven }

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
            else if (character == Character.Three)
            {
                if (type == Type.Greeting)
                {
                    result = "man, you've got some nice hands?";
                }
                else if (type == Type.Misc)
                {
                    result = "do you know what the capital of slovenia is?";
                }
                else if (type == Type.HasBeer)
                {
                    result = "this beer is almost as good as 'Beer'!";
                }
                else if (type == Type.NeedsBeer)
                {
                    result = "i wonder where the beer is. you should find some";
                }
                else if (type == Type.NoMusicPlaying)
                {
                    result = "a party without music is a vibe distaster!";
                }
                else if (type == Type.MusicPlaying)
                {
                    result = "man, this song takes me back to last party";
                }
                else if (type == Type.LikesMusic)
                {
                    result = "did you pick this song? you should be a dj";
                }
                else if (type == Type.DislikesMusic)
                {
                    result = "no offense, but i think you might have bad taste bro";
                }
            }
            else if (character == Character.Four)
            {
                if (type == Type.Greeting)
                {
                    result = "you wanna see my new houseplant?";
                }
                else if (type == Type.Misc)
                {
                    result = "my friends dont know im a cosplayer";
                }
                else if (type == Type.HasBeer)
                {
                    result = "i dont drink but this 'beer' is splendid";
                }
                else if (type == Type.NeedsBeer)
                {
                    result = "wanna bring me a beer? the cans are too big";
                }
                else if (type == Type.NoMusicPlaying)
                {
                    result = "its kinda quiet in here right? right?";
                }
                else if (type == Type.MusicPlaying)
                {
                    result = "when there's music i dont have to be alone with my thoughts!";
                }
                else if (type == Type.LikesMusic)
                {
                    result = "i'd dance to this song if i had any legs!";
                }
                else if (type == Type.DislikesMusic)
                {
                    result = "wow, this song is bad. like 'joker' bad";
                }
            }

            else if (character == Character.Five)
            {
                if (type == Type.Greeting)
                {
                    result = "what's your name? i forgot my name!";
                }
                else if (type == Type.Misc)
                {
                    result = "MJ isn't the GOAT, he's a homosapien!";
                }
                else if (type == Type.HasBeer)
                {
                    result = "this beer is cold like my prospects!";
                }
                else if (type == Type.NeedsBeer)
                {
                    result = "you know what's good with a beer? beer.";
                }
                else if (type == Type.NoMusicPlaying)
                {
                    result = "it's so quiet. you hear that typing noise too?";
                }
                else if (type == Type.MusicPlaying)
                {
                    result = "i hope this music got cleared legally!";
                }
                else if (type == Type.LikesMusic)
                {
                    result = "what a great song. i could cry.";
                }
                else if (type == Type.DislikesMusic)
                {
                    result = "i think an idiot might have put this song on";
                }
            }

            else if (character == Character.Six)
            {
                if (type == Type.Greeting)
                {
                    result = "do you know the host? who is the host?";
                }
                else if (type == Type.Misc)
                {
                    result = "you ever think we're in a video game?";
                }
                else if (type == Type.HasBeer)
                {
                    result = "a few more of these and i'll be drunk enough to drive!";
                }
                else if (type == Type.NeedsBeer)
                {
                    result = "i came here to drink beer, not dilly dally!";
                }
                else if (type == Type.NoMusicPlaying)
                {
                    result = "where's the boombox? put on a sick chune.";
                }
                else if (type == Type.MusicPlaying)
                {
                    result = "whoever is dj-ing this party is a genius!";
                }
                else if (type == Type.LikesMusic)
                {
                    result = "good song. too bad cassettes are a dying medium.";
                }
                else if (type == Type.DislikesMusic)
                {
                    result = "i don't think this song's the one chief.";
                }
            }

            else if (character == Character.Seven)
            {
                if (type == Type.Greeting)
                {
                    result = "nice place huh? so spacious.";
                }
                else if (type == Type.Misc)
                {
                    result = "you know, we have neighbours in all dimensions!";
                }
                else if (type == Type.HasBeer)
                {
                    result = "with beer i can be drunk. and drunk, i can call my ex!";
                }
                else if (type == Type.NeedsBeer)
                {
                    result = "there's no beer, but i've been drinking some turpentine.";
                }
                else if (type == Type.NoMusicPlaying)
                {
                    result = "no music huh? i guess you could call that fun.";
                }
                else if (type == Type.MusicPlaying)
                {
                    result = "ah, music. you know, i do play the mandolin.";
                }
                else if (type == Type.LikesMusic)
                {
                    result = "they say this song raises your IQ.";
                }
                else if (type == Type.DislikesMusic)
                {
                    result = "i didn't know it was legal to play this song!";
                }
            }

            return result.ToLower().Trim();
        }
    }
}
