namespace AnthropicClient.Tests.Unit.Models;

public class MessageBatchResponseTests : SerializationTest
{
  private const string SampleJson = @"{
    ""id"": ""msgbatch_013Zva2CMHLNnXjNJJKqJ2EF"",
    ""type"": ""message_batch"",
    ""processing_status"": ""in_progress"",
    ""request_counts"": {
      ""processing"": 100,
      ""succeeded"": 50,
      ""errored"": 30,
      ""canceled"": 10,
      ""expired"": 10
    },
    ""ended_at"": ""2024-08-20T18:37:24.100435Z"",
    ""created_at"": ""2024-08-20T18:37:24.100435Z"",
    ""expires_at"": ""2024-08-20T18:37:24.100435Z"",
    ""archived_at"": ""2024-08-20T18:37:24.100435Z"",
    ""cancel_initiated_at"": ""2024-08-20T18:37:24.100435Z"",
    ""results_url"": ""https://api.anthropic.com/v1/messages/batches/msgbatch_013Zva2CMHLNnXjNJJKqJ2EF/results""
  }";


  [Fact]
  public void Constructor_WhenCalled_ItShouldReturnInstanceWithPropertiesSet()
  {
    var result = new MessageBatchResponse();

    result.Should().BeOfType<MessageBatchResponse>();
    result.Id.Should().BeEmpty();
    result.Type.Should().BeEmpty();
    result.ProcessingStatus.Should().BeEmpty();
    result.RequestCounts.Should().BeEquivalentTo(new MessageBatchRequestCounts());
    result.EndedAt.Should().Be(DateTimeOffset.MinValue);
    result.CreatedAt.Should().Be(DateTimeOffset.MinValue);
    result.ExpiresAt.Should().Be(DateTimeOffset.MinValue);
    result.ArchivedAt.Should().Be(DateTimeOffset.MinValue);
    result.CancelInitiatedAt.Should().Be(DateTimeOffset.MinValue);
    result.ResultsUrl.Should().BeEmpty();
  }

  [Fact]
  public void JsonSerialization_WhenDeserialized_ItShouldHaveExpectedValues()
  {
    var result = Deserialize<MessageBatchResponse>(SampleJson);

    result.Should().BeEquivalentTo(new MessageBatchResponse
    {
      Id = "msgbatch_013Zva2CMHLNnXjNJJKqJ2EF",
      Type = "message_batch",
      ProcessingStatus = "in_progress",
      RequestCounts = new MessageBatchRequestCounts
      {
        Processing = 100,
        Succeeded = 50,
        Errored = 30,
        Canceled = 10,
        Expired = 10
      },
      EndedAt = DateTimeOffset.Parse("2024-08-20T18:37:24.100435Z"),
      CreatedAt = DateTimeOffset.Parse("2024-08-20T18:37:24.100435Z"),
      ExpiresAt = DateTimeOffset.Parse("2024-08-20T18:37:24.100435Z"),
      ArchivedAt = DateTimeOffset.Parse("2024-08-20T18:37:24.100435Z"),
      CancelInitiatedAt = DateTimeOffset.Parse("2024-08-20T18:37:24.100435Z"),
      ResultsUrl = "https://api.anthropic.com/v1/messages/batches/msgbatch_013Zva2CMHLNnXjNJJKqJ2EF/results"
    });
  }

  [Fact]
  public void JsonSerialization_WhenSerialized_ItShouldHaveExpectedShape()
  {
    var expectedJson = @"{
      ""id"": ""msgbatch_013Zva2CMHLNnXjNJJKqJ2EF"",
      ""type"": ""message_batch"",
      ""processing_status"": ""in_progress"",
      ""request_counts"": {
        ""processing"": 100,
        ""succeeded"": 50,
        ""errored"": 30,
        ""canceled"": 10,
        ""expired"": 10
      },
      ""ended_at"": ""2024-08-20T18:37:24.100435+00:00"",
      ""created_at"": ""2024-08-20T18:37:24.100435+00:00"",
      ""expires_at"": ""2024-08-20T18:37:24.100435+00:00"",
      ""archived_at"": ""2024-08-20T18:37:24.100435+00:00"",
      ""cancel_initiated_at"": ""2024-08-20T18:37:24.100435+00:00"",
      ""results_url"": ""https://api.anthropic.com/v1/messages/batches/msgbatch_013Zva2CMHLNnXjNJJKqJ2EF/results""
    }";

    var result = Serialize(new MessageBatchResponse
    {
      Id = "msgbatch_013Zva2CMHLNnXjNJJKqJ2EF",
      Type = "message_batch",
      ProcessingStatus = "in_progress",
      RequestCounts = new MessageBatchRequestCounts
      {
        Processing = 100,
        Succeeded = 50,
        Errored = 30,
        Canceled = 10,
        Expired = 10
      },
      EndedAt = DateTimeOffset.Parse("2024-08-20T18:37:24.100435Z"),
      CreatedAt = DateTimeOffset.Parse("2024-08-20T18:37:24.100435Z"),
      ExpiresAt = DateTimeOffset.Parse("2024-08-20T18:37:24.100435Z"),
      ArchivedAt = DateTimeOffset.Parse("2024-08-20T18:37:24.100435Z"),
      CancelInitiatedAt = DateTimeOffset.Parse("2024-08-20T18:37:24.100435Z"),
      ResultsUrl = "https://api.anthropic.com/v1/messages/batches/msgbatch_013Zva2CMHLNnXjNJJKqJ2EF/results"
    });

    JsonAssert.Equal(expectedJson, result);
  }
}