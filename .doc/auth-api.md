[Back to README](../README.md)


### Authentication

#### POST /auth/login
- Description: Authenticate a user
- Request Body:
  ```json
  {
    "username": "string",
    "password": "string"
  }
  ```
- Response: 
  ```json
  {
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ],
  "data": {
    "token": "string",
    "email": "string",
    "name": "string",
    "role": "string"
  }
}
  ```

<br/>
<div style="display: flex; justify-content: space-between;">
  <a href="./users-api.md">Previous: Users API</a>
  <a href="./project-structure.md">Next: Project Structure</a>
</div>
