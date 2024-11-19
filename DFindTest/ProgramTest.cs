using DFind;
using JetBrains.Annotations;

namespace DFindTest;

[TestClass]
[TestSubject(typeof(DFind.Program))]
public class ProgramTest
{
    [TestMethod]
    public void BuildOptionsTest()
    {
        string[] args = ["/n", "/i", "/c"];

        var options = DFind.Program.BuildOptions(args);

        Assert.IsNotNull(options);
        Assert.IsTrue(options.IsCaseSensitive);
        Assert.IsTrue(options.IsShowLineNumber);
        Assert.IsTrue(options.IsSkipOfflineFiles);
    }
}