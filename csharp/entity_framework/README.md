# SETUP

## MYSQL in Docker


### Installation with docker
```
docker run --name mysql-docker -p 3306:3306 -e MYSQL_ROOT_PASSWORD=your_password -d mysql:8.0
```

### Accessing MySQL
```
mysql -h 127.0.0.1 -P 3306 -u root -p
```

