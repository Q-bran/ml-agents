import numpy as np
import pandas as pd
import csv
from sklearn.model_selection import train_test_split

dataset_ram = pd.read_csv['C:\Users\bianc\OneDrive\Documents\code\UniBac2\ml-agents\results\soccer\ram_usage_soccer']


#define x and y 
x1= dataset_ram.iloc[:,[0,11]].values
y1= dataset_ram.iloc[[0,10000],:].values   #how many rows ?

#pre processing
#!!!
pd.info()

x_main1,x_test1,y_main1,y_test1 = train_test_split(x1,y1,test_size=200, stratify=y1) #200 being the size of test set 
x_train1,x_val1,y_train1,y_val1 = train_test_split(x_main1,y_main1,test_size=200, stratify=y_main1) #200 being the size of the validation set, the rest being the trainign set.

with open('shuffle_time_soccer.csv', 'w',newline='') as csvfile:
    fieldnames = ['strategy', 'gamePlay', 'batch_size','buffer_size','max_steps','hidden_units','num_layers','total_duration' ]
    writer = csv.writer(csvfile)
    writer.writerows("\nx_main1:\n")
    writer.writerows(x_train1.head())
    writer.writerows(x_train1.shape)

    writer.writerows("\nx_train1:\n")
    writer.writerows(x_train1.head())
    writer.writerows(x_train1.shape)

    writer.writerows("\nx_val1:\n")
    writer.writerows(x_train1.head())
    writer.writerows(x_train1.shape)





