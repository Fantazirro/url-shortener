if [ -z "$(sudo docker images -q url-shortener 2> /dev/null)" ]; then
    sudo docker build -t url-shortener .
fi
docker-compose up -d