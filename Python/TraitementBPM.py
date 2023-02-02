# -*- coding: utf-8 -*-
"""
Created on Mon Jan 30 09:43:52 2023

@author: jerem
"""


import json
import matplotlib.pyplot as plt
from datetime import datetime


liste = []
liste2 = []
listeTime = []
listeBpm = []
fname = "bpm_2023-01-30_13-44-39"
filename = "TestBPM"

with open('C:/Users/jerem/Downloads/'+fname+'.json','r') as f:
    for jsonObj in f:
        Dict = json.loads(jsonObj)
        liste.append(Dict)
        
for element in liste:
    then = datetime.strptime(element[0]["timestamp"], '%Y-%m-%d %H:%M:%S')
    for e in element:
        now = datetime.strptime(e["timestamp"], '%Y-%m-%d %H:%M:%S')
        duration = now - then
        duration_in_s = duration.total_seconds()
        listeTime.append(duration_in_s)
        listeBpm.append(int(e["bpm"]))
        
        
string = '"time":' + str(listeTime) + ',"bpm":' + str(listeBpm)

jsonString = '{' + string + '}'
jsonString = json.loads(jsonString)

file = open('C:/Users/jerem/Downloads/'+filename + '.json', "w")
json.dump(jsonString, file)
file.close()

        
with open('C:/Users/jerem/Downloads/'+filename+'.json','r') as fi:
    for jsonObj2 in fi:
        Dict2 = json.loads(jsonObj2)
        liste2.append(Dict2)
        
for elem in liste2:
    plt.plot(elem["time"], elem["bpm"], label='BPM')
    plt.xlabel('Time')
    plt.ylabel('BPM')
    plt.legend()
    plt.title("BPM through time")
    plt.grid(True)
    plt.savefig('C:/Users/jerem/Downloads/'+fname+'_results.jpg', bbox_inches="tight")
    plt.show()