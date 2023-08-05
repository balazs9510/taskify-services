## Prerequisites
1. Docker installed
2. The migrations are copied into the /migrations folder


## Build the image
To run the build, use the following command:

 ``` bash
 docker build -t taskify-db .
 ```
 ## Run the image

To run the image, specify the **user** and **password**, with the **port** to run on:

 ``` bash
 docker run -e POSTGRES_USER=<user> -e POSTGRES_PASSWORD=<pw> --name taskify-db -p <port>:5432 -d taskify-db
```

Exmaple:
 ``` bash
 docker run --name taskify-db -e POSTGRES_USER=taskify_auth -e POSTGRES_PASSWORD=Admin@123  -p 5000:5432 -d taskify-db
```