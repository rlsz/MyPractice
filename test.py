
allPath=list();
with open('allPath.txt') as f:
    while 1:
        line=f.readline()
        if(line.endswith('\n')):
            if(allPath.count(line)<=0):allPath.append(line)
        else:
            break;

allPath.sort()

newStr=""
for temp in allPath:
    newStr+=temp


import json
with open('distinctPath.txt','wt+') as f:
    f.write(newStr)

'''

line=line.rstrip('\n')
json.dump(allPath,f,indent=0)
for text in allPath:
    print(repr(text))

import json

print(repr(json.dumps(ttt)))


sss=sorted(ttt, reverse=True)

input_text=input('test input:')

print("your input:"+input_text)
'''
