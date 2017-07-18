from pathlib import Path
def distinctStrList(filePath):
    pathInfo=Path(filePath)
    newList=list()
    newFileName="distinct_"+pathInfo.stem+".txt"
    with pathInfo.open(mode='rt') as f:
        strList=f.read().splitlines()
        for item in strList:
            if(item not in newList):
                newList.append(item)
    newList.sort()
    with open(newFileName,'wt+') as f:
        f.write("\n".join(newList))

c=True
while c:
    path=input("enter file path or drag it in here:")
    distinctStrList(path)
    c=input("Is distinct another file?(y):")=='y'
