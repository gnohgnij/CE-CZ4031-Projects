"""
contains code for generating the annotations
"""
import json
import PySimpleGUI as sg
import preprocessing
from tkinter import *

#
# GLOBAL VARIABLES
# Drawing Query Plan Starts Here
#

RECT_WIDTH = 90
RECT_HEIGHT = 65
CANVAS_WIDTH = 1000
CANVAS_HEIGHT = 1000

all_operators = []
visual_to_node = {}
instance = None
MAX_DURATION=0

# Information of rectangle
class Operator:
    def __init__(self, x1, x2, y1, y2, operation, information):
        # four corners coordinates
        self.x1 = x1
        self.x2 = x2
        self.y1 = y1
        self.y2 = y2
        self.operation = operation
        self.information = information
        self.center = ((x1+x2)/2,(y1+y2)/2)
        self.children = []

    def add_child(self, child):
        self.children.append(child)

def build_plan(curr_op, json_plan):
    # plan = json.load(json_plan)
    plan = json_plan
    curr_op.operation = plan["Node Type"]
    curr_op.information = get_current_operator_info(plan)

    all_operators.append(curr_op)

    #check if they are child plans
    if "Plans" in plan:
        children_num = len(plan["Plans"])
        for i in range(children_num):
            x1 = (i) * ((curr_op.x2 - curr_op.x1) / children_num)
            x2 = (i + 1) * ((curr_op.x2 - curr_op.x1) / children_num)
            y1 = curr_op.y2 + RECT_WIDTH
            y2 = curr_op.y2 + 2 * RECT_HEIGHT

            child_op = Operator(x1, x2, y1, y2, "", "")
            curr_op.add_child(child_op)
            build_plan(child_op, plan["Plans"][i])

def get_current_operator_info(operator):

    data = operator
    node_type = data['Node Type']
    
    if node_type == 'Bitmap Heap Scan':
        info = node_type + ' operator on '
        info += data['Relation Name']
        
    elif node_type == 'Bitmap Index Scan':
        info = node_type +' operator on '
        info += data['Index Name']
    
    elif node_type == 'Aggregate':
        info = node_type + ' operator'
        if data['Strategy'] == 'Plain' or data['Strategy'] == 'Sorted': #Strategy types: Plain, Sorted, Hashed
            info += "\n" + str(data['Actual Total Time'] - data['Actual Startup Time'])
    
    elif node_type == 'Gather':
        info = node_type + ' operator\n'
        info += str(data['Actual Total Time'] - data['Actual Startup Time'])

    elif node_type == 'Seq Scan':
        info = node_type + ' operator on '
        info += data['Relation Name'] + '\n'
        info += 'Filter on ' + data['Filter'] + '\n'
        info += 'Duration: ' + str(data['Actual Total Time'] - data['Actual Startup Time'])
    
    elif node_type == 'Gather Merge':
        info = node_type + ' operator'
        info += 'Duration: ' + str(data['Actual Total Time'] - data['Actual Startup Time'])
    
    elif node_type == 'Sort':
        info = node_type + ' operator'
        info += 'Duration: ' + str(data['Actual Total Time'] - data['Actual Startup Time'])

    elif node_type == 'Limit':
        info = node_type + ' operator'
        info += 'Duration: ' + str(data['Actual Total Time'] - data['Actual Startup Time'])

    elif node_type == 'Nested Loop':
        info = node_type + ' operator'
        info += 'Duration: ' + str(data['Actual Total Time'] - data['Actual Startup Time'])

    elif node_type == 'Hash Join':
        info = node_type + ' operator'
    
    elif node_type == 'Hash':
        info = node_type + ' operator'

    elif node_type == 'Index Scan':
        info = node_type + ' operator on'
        info += data['Index Name']

    return info

def draw(query_plan):        

    with open(query_plan) as json_file:
        data = json.load(json_file)
        all_operators.clear()

        root_op = Operator(0, CANVAS_WIDTH,10, RECT_HEIGHT, "", "")
        build_plan(root_op, data)

        # actual drawing
        root = Tk()
        root.geometry("1000x1000")
        root.title("Query execution plan")
        frame=Frame(root,width=1000,height=1000)
        frame.pack()
        canvas = Canvas(frame, width=CANVAS_WIDTH, height=CANVAS_HEIGHT,scrollregion=(0,0,1000,1500))
        vbar = Scrollbar(frame, orient=VERTICAL)
        vbar.pack(side=RIGHT, fill=Y)
        vbar.config(command=canvas.yview)
        canvas.config(yscrollcommand=vbar.set)
        canvas.pack()
        canvas2 = Canvas(frame, width=400, height=100)
        canvas2.pack()
        canvas2.place(x=600,y=0)
        Misc.lift(canvas2)
        Misc.lift(vbar)

        # 3 different for loops are needed for logical binding of rectangles in the node_list
        for element in all_operators:
            x = element.center[0]
            y = element.center[1]
            rect = canvas.create_rectangle(x - RECT_WIDTH / 2, y + RECT_HEIGHT / 2, x + RECT_WIDTH / 2, y - RECT_HEIGHT / 2,
                                        fill='grey', tags="hover")
            visual_to_node[rect] = element

        for element in all_operators:
            gui_text = canvas.create_text((element.center[0], element.center[1]), text=element.information, tags="clicked")
            visual_to_node[gui_text] = element

        for element in all_operators:
            for child in element.children:
                canvas.create_line(child.center[0], child.center[1] - RECT_HEIGHT / 2, element.center[0],
                                element.center[1] + RECT_HEIGHT / 2, arrow=LAST)

        # canvas.tag_bind("clicked", "<Button-1>", lambda event: clicked(event, canvas=canvas2))
        canvas.tag_bind("hover","<Enter>",lambda event:enter(event,canvas=canvas2))
        canvas.tag_bind("hover", "<Leave>", lambda event: leave(event,canvas=canvas2))
        root.mainloop()

def enter(event,canvas):
    node = visual_to_node[event.widget.find_withtag("current")[0]]
    global instance
    if node.duration == MAX_DURATION:
        instance = canvas.create_text(200, 50, text=node.plan_info,
                                      fill="red", width=350)
    else:
        instance = canvas.create_text(200, 50, text=node.plan_info,
                                      fill="blue", width=350)

def leave(event,canvas):
    canvas.delete(instance)

if __name__ == '__main__':

    draw("query_plan.json")

