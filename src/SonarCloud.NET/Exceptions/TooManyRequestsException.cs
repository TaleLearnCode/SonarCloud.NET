using System.Runtime.Serialization;

namespace TaleLearnCode.SonarCloudNet.Exceptions;

/// <summary>
/// The exception thrown when attempting to make too many requests to the SonarCloud Web API within a specified time.
/// Implements the <see cref="Exception" />
/// </summary>
/// <seealso cref="Exception" />
/// <remarks>There is no documentation on the API rate limit. The documentation states that the API returns HTTP status 429 when the user reaches the rate limit and that if this happens, wait a few minutes before retrying the operation.</remarks>
[Serializable]
public class TooManyRequestsException : Exception
{

  private const string _defaultMessage = "The SonarCloud Web API request has not been completed because it lacks valid authentication credentials for the requested resource.";

  /// <summary>
  /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class with the default message.
  /// </summary>
  public TooManyRequestsException() : base(_defaultMessage) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class with a specified error message.
  /// </summary>
  /// <param name="message">The message that describes the error.</param>
  public TooManyRequestsException(string message) : base(message) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class with a specified error message and a reference to the exception that is the cause of this exception.
  /// </summary>
  /// <param name="message">The error message that explains the reason for the exception.</param>
  /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
  public TooManyRequestsException(string message, Exception innerException) : base(message, innerException) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class with the default message and a reference to the exception that is the cause of this exception.
  /// </summary>
  /// <param name="innerException">The inner exception.</param>
  public TooManyRequestsException(Exception innerException) : base(_defaultMessage, innerException) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class.
  /// </summary>
  /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
  /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
  protected TooManyRequestsException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}