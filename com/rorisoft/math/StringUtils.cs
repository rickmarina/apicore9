namespace com.rorisoft.math
{
    public class StringUtils
    {

        public static IEnumerable<string> Permutations(string word)
        {
            if (word.Length > 1)
            {
                char character = word[0];
                foreach (string subPermute in Permutations(word.Substring(1)))
                {

                    for (int index = 0; index <= subPermute.Length; index++)
                    {
                        string pre = subPermute.Substring(0, index);
                        string post = subPermute.Substring(index);

                        if (post.Contains(character))
                            continue;

                        yield return pre + character + post;
                    }

                }
            }
            else
            {
                yield return word;
            }
        }
    }
}
