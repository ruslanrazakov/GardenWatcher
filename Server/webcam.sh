#!/bin/bash

while [ : ]
do
	fswebcam -d /dev/video1 -r 1920x1080 -S 30 -F 5 /home/rus/Server/publish/wwwroot/Photos/$(date +%Y-%m-%d-%H%M).jpg
	sleep 24h
done

