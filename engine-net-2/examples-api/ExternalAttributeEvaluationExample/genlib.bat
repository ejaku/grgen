..\..\bin\grgen -keep . ..\..\examples\ExternalAttributeEvaluationExample\ExternalAttributeEvaluation.grg
copy ..\..\examples\ExternalAttributeEvaluationExample\ExternalAttributeEvaluationModelExternalFunctions.cs ..\..\examples-api\ExternalAttributeEvaluationExample\ExternalAttributeEvaluationModelExternalFunctions.cs
@if ERRORLEVEL 1 PAUSE
