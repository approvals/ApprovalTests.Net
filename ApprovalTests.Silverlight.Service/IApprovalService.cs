using System.ServiceModel;
using System.ServiceModel.Web;

namespace ApprovalTests.Silverlight.Service
{
	[ServiceContract]
	public interface IApprovalService
	{
		[OperationContract]
		[WebInvoke(Method = "PUT", UriTemplate = "Approve")]
		void Approve(string path, string testName, byte[] content);
	}
}
