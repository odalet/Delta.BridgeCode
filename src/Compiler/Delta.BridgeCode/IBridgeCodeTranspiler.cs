
namespace Delta.BridgeCode
{
    public interface IBridgeCodeTranspiler
    {
        string Transpile(BridgeCodeSyntaxTree tree);
    }
}
