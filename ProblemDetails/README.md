# Problem Details issues

## Set content-type to `application/problem+json` for generic TypedResults with T=ProblemDetails

Currently when an endpoint returns a generic TypedResults with T=ProblemDetails, such as `NotFound<ProblemDetails>`,
the content-type is set to `application/json` instead of `application/problem+json`.

The endpoint _could_ set the content-type manually, but it would be nice if the framework could do this automatically.

Note that the generated OpenAPI document currently shows the response as `application/json`, which is correct
as long as the endpoint does not set the content-type manually.

So the ask here is to make the content-type `application/problem+json` for TypedResults with T=ProblemDetails
_and_ to reflect this in the OpenAPI document.
