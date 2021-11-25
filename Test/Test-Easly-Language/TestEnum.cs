namespace TestEaslyLanguage
{
class SomeBaseClass
{}

class Descendant1 : SomeBaseClass
{}

class Descendant2 : SomeBaseClass
{}

class TestEnum
{
public bool Test(SomeBaseClass inputValue, out object outputValue)
{
    outputValue = null!;
    bool Result = default;
    bool IsHandled = false;

    switch (inputValue)
    {
    case Descendant1 AsDescendant1:
        Result = Test1(AsDescendant1, out outputValue);
        IsHandled = true;
        break;
    case Descendant2 AsDescendant2:
        Result = Test2(AsDescendant2, out outputValue);
        IsHandled = true;
        break;
    }

    System.Diagnostics.Debug.Assert(IsHandled);

    return Result;
}

        public bool Test1(Descendant1 inputDescendant1, out object outputValue)
        {
            //...
            outputValue = null!;
            return true;
        }

        public bool Test2(Descendant2 inputDescendant2, out object outputValue)
        {
            //...
            outputValue = null!;
            return true;
        }
    }
}
