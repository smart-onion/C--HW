using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;
using System.Text;
using System.Net.Sockets;

namespace Shared
{
    public delegate void Request(NetworkStream ns);
    static public class Utility
    {
        static public string ReadStringFromStream(NetworkStream ns)
        {
            byte[] data = new byte[1024];
            int bytes;
            StringBuilder responseData = new StringBuilder();
            do
            {
                bytes = ns.Read(data, 0, data.Length);
                responseData.Append(Encoding.ASCII.GetString(data, 0, bytes));
            }
            while (ns.DataAvailable);
            return responseData.ToString();
        }
    }
}
