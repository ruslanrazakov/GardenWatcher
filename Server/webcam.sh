#!/bin/bash

while [ : ]
do
	fswebcam -r 1280x720 -d /dev/video1 /home/rus/Photos/$(date +%Y-%m-%d-%H%M).jpg
	sleep 24h
done

