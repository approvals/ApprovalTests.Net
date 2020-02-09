[Approvals.Verify(IApprovalWriter writer, IApprovalNamer namer, IApprovalFailureReporter reporter)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs)  
  
[Approvals.VerifyFile(String receivedFilePath)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs)  
  
[Approvals.VerifyWithCallback(Object text, Action&lt;String> callBackOnFailure)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs)  
  
[Approvals.VerifyWithExtension(String text, String fileExtensionWithDot, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs)  
  
[Approvals.VerifyException(Exception e)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs)  
  
[Approvals.VerifyExceptionWithStacktrace(Exception e)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs)  
  
[Approvals.VerifyAll(String header, IEnumerable&lt;T> enumerable, String label)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs)  
  
[Approvals.VerifyBinaryFile(Byte[] bytes, String fileExtensionWithDot)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs)  
  
[Approvals.VerifyHtml(String html)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs)  
  
[Approvals.VerifyXml(String xml)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs)  
  
[Approvals.VerifyJson(String json)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs)  
  
[Approvals.VerifyPdfFile(String pdfFilePath)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs)  
  
[XmlApprovals.VerifyXml(String xml, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Xml/XmlApprovals.cs)  
  
[XmlApprovals.VerifyText(String text, String fileExtensionWithoutDot, Boolean safely, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Xml/XmlApprovals.cs)  
  
[XmlApprovals.VerifyOrderedXml(String text, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Xml/XmlApprovals.cs)  
  
[SetApprovals.VerifySet(IEnumerable&lt;T> enumerable, Func&lt;T, String> formatter)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Set/SetApprovals.cs)  
  
[SetApprovals.VerifyFileAsSet(String filename, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Set/SetApprovals.cs)  
  
[DatabaseApprovals.Verify(IDatabaseToExecuteableQueryAdaptor adapter)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Persistence/DatabaseApprovals.cs)  
  
[ThreadSafteyTheory.VerifyNoRaceConditions(Int32 times, Func&lt;T> caseGenerator, Func&lt;T, String> caseString, Func&lt;T, Object> possibleRaceConditonFunction, Func&lt;T, Object> knownGoodFunction)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/TheoryTests/ThreadSafteyTheory.cs)  
  
[SerializableTheory.Verify(Object original, Action&lt;Object, Object> assertEqual)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/TheoryTests/SerializableTheory.cs)  
  
[ThreadSafetyTheory.VerifyNoRaceConditions(Int32 times, Func&lt;T> caseGenerator, Func&lt;T, String> caseString, Func&lt;T, Object> possibleRaceConditionFunction, Func&lt;T, Object> knownGoodFunction)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/TheoryTests/ThreadSafetyTheory.cs)  
  
[AsyncApprovals.VerifyException(Task task)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Async/AsyncApprovals.cs)  
  
[AsyncApprovals.Verify(Func&lt;Task&lt;T>> taskRunner)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Async/AsyncApprovals.cs)  
  
[ApprovalMaintenance.VerifyNoAbandonedFiles(String[] ignore)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Maintenance/ApprovalMaintenance.cs)  
  
[HtmlApprovals.VerifyHtml(String html, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Html/HtmlApprovals.cs)  
  
[HtmlApprovals.VerifyHtmlStrict(String html)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Html/HtmlApprovals.cs)  
  
[EventApprovals.VerifyEvents(Object value)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Events/EventApprovals.cs)  
  
[EmailApprovals.Verify(MailMessage email)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Email/EmailApprovals.cs)  
  
[EmailApprovals.VerifyScrubbed(MailMessage email, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Email/EmailApprovals.cs)  
  
[Approver.Verify(IApprovalApprover approver, IApprovalFailureReporter reporter)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Core/Approver.cs)  
  
[CombinationApprovals.VerifyAllCombinations(Func&lt;A, Object> processCall, IEnumerable&lt;A> aList)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Combinations/CombinationApprovals.cs)