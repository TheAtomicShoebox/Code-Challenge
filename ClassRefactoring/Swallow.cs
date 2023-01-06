using System;

// I split this file into multiple files
// I avoid having multiple classes/etc in one file.
// I renamed the file so that the class name and the file are the same

namespace DeveloperSample.ClassRefactoring
{
    public class Swallow
    {
        /// <summary>
        ///     Changed the name here because it conflicts with <see cref="Type"/>
        /// </summary>
        public SwallowType SwallowType { get; }

        /// <summary>
        ///     Changed the name here to be more parallel with <see cref="SwallowType"/>
        /// </summary>
        public SwallowLoad SwallowLoad { get; private set; }

        // I added a parameter here
        public Swallow(SwallowType swallowType, SwallowLoad swallowLoad)
        {
            SwallowType = swallowType;
            SwallowLoad = swallowLoad;
        }
        
        // Is this really necessary?
        // I commented out this call instead of deleting it to show that I removed this concept.
        // In the unit tests, there's no situation in which we have one load and change to another, so this didn't really make sense to me
        // This does mean that I slightly altered the unit tests, but that should be fine
        //public void ApplyLoad(SwallowLoad load)
        //{
        //    SwallowLoad = load;
        //}

        public double GetAirspeedVelocity()
        {
            // I used ternary expressions because I like them and they remove some space; also, because there were only 2 options for load
            // With more options for load, I would have gone for a secondary case statement,
            // or perhaps locked down the most relevant/unique combinations and used fall-through
            switch (SwallowType)
            {
                case SwallowType.African:
                    return SwallowLoad == SwallowLoad.None ? 22 : 18;
                case SwallowType.European:
                    return SwallowLoad == SwallowLoad.None ? 20 : 16;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}