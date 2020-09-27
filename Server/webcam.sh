#!/bin/bash

while [ : ]
do
	fswebcam -d /dev/video0 -r 1920x1080 -S 30 -F 5 /home/rus/Server/wwwroot/Photos/$(date +%Y-%m-%d-%H%M).jpg
	sleep 1h
done

