#! python3
import sys

from pathlib import Path



root=input("enter root path:")
root=Path(root).as_posix()
allFiles =sorted(Path(root).glob("**/*"))


import os

for item in allFiles:
    target=item.as_posix().replace(root,'temp1')
    if item.is_file():
        with open(item.as_posix(),'rb') as f:
            
            with open(target,'wb+') as t:
                t.write(f.read())
    elif item.is_dir():
        Path(target).mkdir(parents=True,exist_ok=True)
    
input("press any key to exit")






'''
copy folder to temp1

sys.stdout.write("hello from Python {0} hahaha".format(sys.version))
print('\r\n')
with open('aaaa.txt','rt') as f:
    print(f.read())
test={'aaa':'asda','bbb':'dwdzxcq'}

print(repr(test))
import json
with open('bbbb.txt','wt') as bb:
    bb.write(json.dumps(test))
with open('cccc.txt','wt') as cc:
    json.dump(test,cc)
'''
