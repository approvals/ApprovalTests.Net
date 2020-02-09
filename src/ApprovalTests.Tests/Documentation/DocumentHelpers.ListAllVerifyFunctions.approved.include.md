[Approvals.Verify(IApprovalWriter writer, IApprovalNamer namer, IApprovalFailureReporter reporter)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs#L45)  
  
[Approvals.VerifyFile(String receivedFilePath)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs#L125)  
  
[Approvals.VerifyWithCallback(Object text, Action&lt;String> callBackOnFailure)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs#L135)  
  
[Approvals.VerifyWithExtension(String text, String fileExtensionWithDot, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs#L177)  
  
[Approvals.VerifyException(Exception e)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs#L196)  
  
[Approvals.VerifyExceptionWithStacktrace(Exception e)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs#L201)  
  
[Approvals.VerifyAll(String header, IEnumerable&lt;T> enumerable, String label)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs#L210)  
  
[Approvals.VerifyBinaryFile(Byte[] bytes, String fileExtensionWithDot)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs#L258)  
  
[Approvals.VerifyHtml(String html)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs#L264)  
  
[Approvals.VerifyXml(String xml)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs#L269)  
  
[Approvals.VerifyJson(String json)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs#L274)  
  
[Approvals.VerifyPdfFile(String pdfFilePath)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Approvals.cs#L294)  
  
[XmlApprovals.VerifyXml(String xml, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Xml/XmlApprovals.cs#L10)  
  
[XmlApprovals.VerifyText(String text, String fileExtensionWithoutDot, Boolean safely, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Xml/XmlApprovals.cs#L18)  
  
[XmlApprovals.VerifyOrderedXml(String text, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Xml/XmlApprovals.cs#L29)  
  
[SetApprovals.VerifySet(IEnumerable&lt;T> enumerable, Func&lt;T, String> formatter)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Set/SetApprovals.cs#L15)  
  
[SetApprovals.VerifyFileAsSet(String filename, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Set/SetApprovals.cs#L40)  
  
[DatabaseApprovals.Verify(IDatabaseToExecuteableQueryAdaptor adapter)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Persistence/DatabaseApprovals.cs#L10)  
  
[SerializableTheory.Verify(Object original, Action&lt;Object, Object> assertEqual)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/TheoryTests/SerializableTheory.cs#L9)  
  
[ThreadSafetyTheory.VerifyNoRaceConditions(Int32 times, Func&lt;T> caseGenerator, Func&lt;T, String> caseString, Func&lt;T, Object> possibleRaceConditionFunction, Func&lt;T, Object> knownGoodFunction)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/TheoryTests/ThreadSafetyTheory.cs#L13)  
  
[ApprovalMaintenance.VerifyNoAbandonedFiles(String[] ignore)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Maintenance/ApprovalMaintenance.cs#L77)  
  
[HtmlApprovals.VerifyHtml(String html, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Html/HtmlApprovals.cs#L8)  
  
[HtmlApprovals.VerifyHtmlStrict(String html)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Html/HtmlApprovals.cs#L16)  
  
[EventApprovals.VerifyEvents(Object value)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Events/EventApprovals.cs#L10)  
  
[EmailApprovals.Verify(MailMessage email)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Email/EmailApprovals.cs#L13)  
  
[EmailApprovals.VerifyScrubbed(MailMessage email, Func&lt;String, String> scrubber)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Email/EmailApprovals.cs#L17)  
  
[Approver.Verify(IApprovalApprover approver, IApprovalFailureReporter reporter)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Core/Approver.cs#L5)  
  
[CombinationApprovals.VerifyAllCombinations(Func&lt;A, Object> processCall, IEnumerable&lt;A> aList)](https://github.com/approvals/ApprovalTests.Net/blob/master/src/ApprovalTests/Combinations/CombinationApprovals.cs#L12)