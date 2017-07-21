import subprocess

def getpid_windows(process_name):
    """利用cmd_str = tasklist|find /i "xdict.exe" 来查找windows平台的进程id"""
    cmd_line = 'tasklist|find /i "%s"' %process_name
    pp = subprocess.Popen(cmd_line,shell=True,stdout=subprocess.PIPE,stderr=subprocess.PIPE)
    out,err = pp.communicate()
    if pp.returncode != 0:
        print('error:'+err)
        return -1

    elif out.strip == '': # 这个情况是针对查询的进程没有启动的情况
        print('error:find process does not srart')
        return -1
    else:
        out_str = out.strip()
        pid = out_str[30:34]
        return int(pid)

def getpid_linux(process_name):
    """利用 ps -ef|grep %s|grep -v grep|awk '{print $2}' 来查找linux 平台的进程id"""
    cmd_line = "ps -ef|grep %s|grep -v grep|awk '{print $2}'" %process_name
    pp = subprocess.Popen(cmd_line,shell=True,stdout=subprocess.PIPE,stderr=subprocess.PIPE)
    out,err = pp.communicate()
    if pp.returncode != 0:
        print('error:'+err)
        return -1
    elif out.strip() == "":
        print('error:find process does not srart')
        return -1  
    else:
        pid = out.strip()
        return int(pid)

import signal
import os
if __name__ == '__main__':
    c=True
    while c:
        pname=input("input process name which you want to kill:")
        pid = getpid_windows(pname)
        try:
            os.kill(pid,signal.CTRL_C_EVENT)
        except Exception as error:
            print(error)
        c=input("Is kill another process?(y):")=='y'
    





'''
import os
#print(os.environ['HOME'])
rootPath=os.getcwd()

for entry in os.scandir('.'):
   if not entry.name.startswith('.') and entry.is_file():
       print(entry.name)

import json
configPath=rootPath+"\myConfig.json"
try:
    fp=open(configPath,"rt")
except Exception as error:
    print(error)
else:
    with fp:
        configs=json.load(fp)
        print("read config:"+configPath)
        print(json.dumps(configs,indent=2))
        for config in configs:
            for module in config['modules']:
                os.chdir(config['root'])
                os.chdir(module)
                print("enter path:"+os.getcwd())
                print("excute command::"+config['command'])
                #os.system(config['command'])
                print("command complete!")

input("press any key to exit")

'''

'''
if os.access("myfile", os.R_OK):
    with open("myfile") as fp:
        return fp.read()
return "some default data"



from urllib import request,parse

local_filename, headers = request.urlretrieve('http://weibo.com/u/3057179881?refer_flag=1001030101_&is_hot=1')
html = open(local_filename)
print(html.read())
print(local_filename)

url='http://weibo.com/u/3057179881'
values = {'refer_flag' : '1001030101_',
          'is_hot' : 1 }
data = parse.urlencode(values)
full_url = url + '?' + data
data = data.encode('ascii')
req = request.Request(url, data)
with request.urlopen(req) as response:
   the_page = response.read()
   print(the_page)








with request.urlopen('http://weibo.com/u/3057179881?refer_flag=1001030101_&is_hot=1') as response:
    html=response.read()
    print(html)
    

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
