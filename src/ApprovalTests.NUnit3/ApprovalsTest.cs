﻿[TestFixture]
public class ApprovalsTest
{
    static readonly string[] text = ["abc", "123", "!@#"];

    // begin-snippet: simple_verify
    [Test]
    public void Text() =>
        Approvals.Verify("should be approved");
    // end-snippet

    [Test]
    public void VerifyWithExtension() =>
        Approvals.VerifyWithExtension("should,be,approved", ".csv");

    [Test]
    public void EnumerableWithLabel() =>
        Approvals.VerifyAll(text, "collection");

    [Test]
    public void TestExistingFile()
    {
        var path = PathUtilities.GetDirectoryForCaller();
        var copy = path + "copyOfa.txt";
        File.Copy(path + "a.txt", copy, true);
        Approvals.VerifyFile(copy);
    }

    [Test]
    public void TestBytes()
    {
        var path = PathUtilities.GetDirectoryForCaller();
        Approvals.VerifyBinaryFile(File.ReadAllBytes(path + "a.png"), "png");
    }

    [Test]
    public void EnumerableWithLabelAndFormatter() =>
        Approvals.VerifyAll(text, "collection", t => "" + t.Length);

    [Test]
    public void EnumerableWithHeaderAndFormatter()
    {
        var word = "Llewellyn";
        Approvals.VerifyAll(word, word.ToCharArray(), c => c + " => " + (int) c);
    }

    [Test]
    public void VerifyAllWithNull()
    {
        var words = new[] {"abc", null, "123", "!@#"};
        Approvals.VerifyAll(words, "");
    }

    [Test]
    public void VerifyAllNull()
    {
        string[] words = null;
        Approvals.VerifyAll(words, "words");
    }

    [Test]
    public void VerifyNullDictionary()
    {
        Dictionary<string, int> words = null;
        Approvals.VerifyAll(words);
    }

    [Test]
    public void DictionarySimple() =>
        Approvals.VerifyAll(FireFlyMap());

    [Test]
    public void Dictionary() =>
        Approvals.VerifyAll("Firefly", FireFlyMap());

    [Test]
    public void DictionaryCustom() =>
        Approvals.VerifyAll("Firefly", FireFlyMap(), (k, v) => $"\"{k}\" => {v}");

    [Test]
    public void DictionaryCustomNoHeader() =>
        Approvals.VerifyAll(FireFlyMap(), (k, v) => $"\"{k}\" => {v}");

    static Dictionary<string, string> FireFlyMap() =>
        new()
        {
            {"Caption", "Mal"},
            {"2nd In Command", "Zoey"},
            {"Pilot", "Wash"},
            {"Companion", "Inara"},
            {"Muscle", "Jayne"},
            {"Mechanic", "Kaylee"},
            {"Doctor", "Simon"},
            {"Pastor", "Book"},
            {"Stowaway", "River"}
        };

    [Test]
    public void EnumerableWithFormatter() =>
        Approvals.VerifyAll(text, t => "" + t.Length);

    [Test]
    public void JsonText()
    {
        var json = """{"GivenNames":"John","FamilyName":"Smith","Spouse":"Jill","Address":{"Street":"1 Puddle Lane","Suburb":null,"Country":"USA"},"Children":["Sam","Mary"],"Title":"Mr"}""";
        Approvals.VerifyJson(json);
    }
}