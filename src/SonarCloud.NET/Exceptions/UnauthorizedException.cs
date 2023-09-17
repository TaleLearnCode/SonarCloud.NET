using System.Runtime.Serialization;

namespace TaleLearnCode.SonarCloudNet.Exceptions;

/// <summary>
/// The exception thrown when attempting to make a request to the SonarCloud Web API without valid authentication credentials.
/// Implements the <see cref="Exception" />
/// </summary>
/// <seealso cref="Exception" />
[Serializable]
public class UnauthorizedException : Exception
{

  private const string _defaultMessage = "The SonarCloud Web API request has not been completed because it lacks valid authentication credentials for the requested resource.";

  /// <summary>
  /// Initializes a new instance of the <see cref="UnauthorizedException"/> class with the default message.
  /// </summary>
  public UnauthorizedException() : base(_defaultMessage) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="UnauthorizedException"/> class with a specified error message.
  /// </summary>
  /// <param name="message">The message that describes the error.</param>
  public UnauthorizedException(string message) : base(message) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="UnauthorizedException"/> class with a specified error message and a reference to the exception that is the cause of this exception.
  /// </summary>
  /// <param name="message">The error message that explains the reason for the exception.</param>
  /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
  public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="UnauthorizedException"/> class with the default message and a reference to the exception that is the cause of this exception.
  /// </summary>
  /// <param name="innerException">The inner exception.</param>
  public UnauthorizedException(Exception innerException) : base(_defaultMessage, innerException) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
  /// </summary>
  /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
  /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
  protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}