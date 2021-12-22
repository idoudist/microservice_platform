/***********welcome to the docker setup command**********/

#build
docker build -t idoudist/platformservice .

#runImage 
docker run -p 8080:80 -d idoudist/platformservice
# 8080:80 external internal port
# -d stand for detatched
# idoudist/platformservice is the docker image name

#Show running containers
docker ps 

#stop container
docker stop <containerId>
# <containerId> you can get it when executing "docker ps"

#start an existing container
docker start <containerId>

#push to docker hub
docker push idoudist/platformservice