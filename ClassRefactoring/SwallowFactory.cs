namespace DeveloperSample.ClassRefactoring
{
    public class SwallowFactory
    {
        // Does this really get a swallow or just create on every time? What's the use case? Is it important to hold onto an existing one?
        // Is there a requirement not to call constructors, perhaps because of an existing pattern?
        // In any case, I refactored to add braces out of personal preference
        public Swallow GetSwallow(SwallowType swallowType, SwallowLoad swallowLoad = SwallowLoad.None)
        {
            return new Swallow(swallowType, swallowLoad);
        }
    }
}
