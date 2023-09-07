using System.Net.Mail;

[TestFixture]
[UseReporter(typeof(MachineSpecificReporter))]
public class EmailTest
{
    [Test]
    public void Testname()
    {
        using (ApprovalResults.UniqueForOs())
        {
            var message = new MailMessage();
            message.To.Add("approvals@approvaltests.com");
            message.Subject = "this project rocks";
            message.From = new("everybody@acomputer.com");
            message.Body = """
                           Wow, this is so cool.
                           I should send more emails.
                           thanks,
                           your biggest fan
                           """;
            EmailApprovals.Verify(message);
        }
    }

#if(NET48)
        [Test]
        public void TestAttachment()
        {
            using (ApprovalResults.UniqueForOs())
            {
                var message = new MailMessage();
                message.To.Add("approvals@approvaltests.com");
                message.Subject = "this project rocks";
                message.From = new("everybody@acomputer.com");
                message.Body = "Pictures Attached";
                message.Attachments.Add(new(PathUtilities.GetAdjacentFile("tower.png")));
                message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString("This be alternate."));
                EmailApprovals.Verify(message);
            }
        }
#endif

    [Test]
    public void TestCollectBoundaries()
    {
        var text = """
                   Content-Type: multipart/mixed; boundary=--boundary_1_f3c617c1-4388-492c-8e07-0ef9bdb8af93

                   ----boundary_1_f3c617c1-4388-492c-8e07-0ef9bdb8af93
                   Content-Type: multipart/alternative; boundary=--boundary_0_283b02d8-4af3-4d14-83bd-6d7181499f83


                   """;
        Approvals.VerifyAll(EmailApprovals.FindBoundaries(text), "boundary");
    }
}