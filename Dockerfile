
FROM mono:onbuild
ENV SYSTEM_NAME="lighthouse"
ENV IP=""
ENV PORT=""
ENV SEEDS="[]"

CMD [ "mono",  "./Lighthouse.exe" ]