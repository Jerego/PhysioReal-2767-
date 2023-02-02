# -*- coding: utf-8 -*-
"""
Created on Mon Jan 30 10:26:43 2023

@author: jerem
"""

import json
import matplotlib.pyplot as plt

liste = []
fname = "historic3"
filename = "valuesDistance.json"

with open('C:/Users/jerem/Downloads/'+fname+'.json','r') as f:
    for jsonObj in f:
        Dict = json.loads(jsonObj)
        liste.append(Dict)
        
print("Printing result")
for element in liste:
    print("name: " + element["name"])
    print("accuracy: " + str(round(element["accuracy"]*100,1)))
    print("score: " + str(element["score"]))
    plt.plot(element["musicTimer"], element["distanceRightHand"], label='right')
    plt.plot(element["musicTimer"], element["distanceLeftHand"], label='left')
    plt.xlabel('Time')
    plt.ylabel('Distance')
    plt.legend()
    plt.title("Hands' position according to the time")
    plt.grid(True)
    plt.savefig('C:/Users/jerem/Downloads/'+fname+'_results.jpg', bbox_inches="tight")
    plt.show()
    MinR = str(min(element["distanceRightHand"]))
    MaxR = str(max(element["distanceRightHand"]))
    AvgR = str(round(sum(element["distanceRightHand"])/len(element["distanceRightHand"]),2))
    MinL = str(min(element["distanceLeftHand"]))
    MaxL = str(max(element["distanceLeftHand"]))
    AvgL = str(round(sum(element["distanceLeftHand"])/len(element["distanceLeftHand"]),2))
    string = '"MinR":' + MinR +',"MaxR":' + MaxR +',"AvgR":' + AvgR + ',"MinL":' + MinL +',"MaxL":' + MaxL +',"AvgL":' + AvgL
    jsonString = '{' + string + '}'
    jsonString = json.loads(jsonString)

    file = open('C:/Users/jerem/Downloads/'+filename, "w")
    json.dump(jsonString, file)
    file.close()