from webbrowser import open as openWebrowser
from os.path import realpath as realPath
import matplotlib.pyplot as plt
from sklearn import preprocessing
import numpy as np

print("Loading results...")

output = [i for i in open("Out/output.txt", "r", encoding="utf-8").readlines()]

tableOfOutput = ""

print("Creating website...")

pawnTypes = ['p', 'r', 'n', 'b', 'q', 'k']

everyPawnsBoard = {}

for pawnType in pawnTypes:
    everyPawnsBoard[pawnType] = []

for i in range(0, len(output), 20):
    # get last element of line expect new line symbol => \n
    # w;2;2;p\n
    #       ^
    pawnType = output[i][-2:-1]
    # get board almost without frame and \n
    # each every 20 lines we got new pawn input with output
    # board take 19 lines
    # every second lane have field (others are frame or coordinates)

    b = [output[i + j][2:-1] for j in range(3, 19, 2)]
    # segregate every board by pawn type
    everyPawnsBoard[pawnType].append(b)

# create table
for i in range(0, len(output), 20):
    tableOfOutput += "<tr><td>" + str(output[i]) + "</td><td><pre>" + "".join(
        output[i + j] for j in range(1, 19)) + "</pre></td></tr>"

newIndex = open("html\index_template.html", "r", encoding="utf-8").read().replace(
    "TableHere", tableOfOutput)

# save page
open("wwwroot\index.html", "w", encoding="utf-8").write(newIndex)

print("Creating heatmaps...")


# initialize matrix for X counter
tempMatrix = {}

for pawnType in pawnTypes:
    tempMatrix[pawnType] = [[0 for i in range(8)] for j in range(8)]

# for every pawn's type
for key, value in everyPawnsBoard.items():
    # for every board of pawn
    for board in value:
        # on each line
        for x in range(8):
            if "X" not in board[x]:
                continue

            # every line is 4 * 8 long (frame + 3 spaces (or 2 spaces and X or pawn))
            # center of field can contains X, space, pawn
            # 2 |_ _ _| _ X _ | _ X _ | _ _ _ | _ ....
            #      ^      ^
            #      \______/
            #         ^ 4 char distance between centers of fields
            for y in range(2, 33, 4):
                if board[x][y] == "X":
                    tempMatrix[key][x][(y - 2) // 4] += 1

print("Normalize data...")

normMatrix = {}
# normalize
for key, value in tempMatrix.items():
    normMatrix[key] = np.array([[round(j, 2) for j in i]
                                for i in preprocessing.normalize(value)])


fig, ax = plt.subplots(nrows=2, ncols=3, figsize=(12, 18))

# axis' names
yName = [str(i) for i in range(8, 0, -1)]
xName = [chr(ord("A") + i) for i in range(8)]

polishNameForPawns = {'p': "Pion", 'r': "Wieża", 'n': "Skoczek", 
                      'b': "Goniec", 'q': "Hetman", 'k': "Król"}

# create plot for every pawn's type
for ax, pawnType in zip(ax.flat, pawnTypes):
    im = ax.imshow(normMatrix[pawnType], cmap='copper')
    ax.set_title(str(polishNameForPawns[pawnType]))
    ax.tick_params(top=True, bottom=True, labeltop=True, labelbottom=True,
                   left=True, right=True, labelleft=True, labelright=True)

    # We want to show all ticks...
    ax.set_xticks(np.arange(len(xName)))
    ax.set_yticks(np.arange(len(yName)))
    # ... and label them with the respective list entries
    ax.set_xticklabels(xName)
    ax.set_yticklabels(yName)

    # Loop over data dimensions and create text annotations.
    for i in range(len(yName)):
        for j in range(len(xName)):
            ax.text(j, i, normMatrix[pawnType][i, j],
                    ha="center", va="center", color="w")

plt.tight_layout()


# save heatmap
ax.get_figure().savefig('wwwroot\heatmap.png')
print("Opening website...")

openWebrowser("file://" + realPath("wwwroot\index.html"))
