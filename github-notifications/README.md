## Setup

1. Create a [GitHub Personal Access Token](https://github.com/settings/tokens)
1. Open Notepad++ and enter `username:[token]`
1. Select text -> Plugins -> MIME Tools -> Base64 Encode
1. Create/Update ENV Varialbe `"GitHub Personal Access Token"` (try [Rapid Env Editor](https://www.rapidee.com/en/screenshots))

![](readme-resources/2020-01-31-15-04-42.png)

### Background notes
http header Authorization Basic dXNlcm5hbWU6NWY2OGMzN2MzNjM5NmEzNTM5MjgxN2JhODcwNDI5MzI1ZjY5ODNkOQ
which is base64 encoding of personal access token
from fiddler
Authorization Header is present: Basic dXNlcm5hbWU6NWY2OGMzN2MzNjM5NmEzNTM5MjgxN2JhODcwNDI5MzI1ZjY5ODNkOQ==

Decoded Username:Password = username:5f68c37c36396a35392817ba870429325f6983d9