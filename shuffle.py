import numpy as np
import pandas as pd
import random
import csv

dataset_ram = pd.read_csv['C:\Users\bianc\OneDrive\Documents\code\UniBac2\ml-agents\results\soccer\ram_usage_soccer']
dataset_time = pd.read_csv['C:\Users\bianc\OneDrive\Documents\code\UniBac2\ml-agents\results\soccer\time_per_training']


ram = dataset_ram.readlines()
time = dataset_time.readlines()

#write to new csv 
with open('shuffle_ram_soccer.csv', 'w',newline='') as csvfile:
    fieldnames = ['training_run_number','strategy', 'gamePlay', 'batch_size','buffer_size','max_steps','hidden_units','num_layers','CPU_count','CPU_freq_GHz','Total_Phys_RAM_GB','RAM_KB' ]
    writer = csv.writer(csvfile)
    writer.writerows(random.shuffle(ram))

with open('shuff;e_time_soccer.csv', 'w',newline='') as csvfile:
    fieldnames = ['strategy', 'gamePlay', 'batch_size','buffer_size','max_steps','hidden_units','num_layers','total_duration' ]
    writer = csv.writer(csvfile)
    writer.writerows(random.shuffle(time))


#max_steps = 12 mill = end training run 