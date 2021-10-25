"""
contains code for GUI
"""

import PySimpleGUI as sg
from preprocessing import *

class GUI():

    def __init__(self):
        return

    def initialise_GUI(self):

        sg.theme('LightGrey1')   #theme of GUI
        font = 'Consolas'

        # All the stuff inside the window.
        col1 = [
            [sg.Text('Host:', font=(font, 12), justification='left')],
            [sg.Text('Port:', font=(font, 12), justification='left')],
            [sg.Text('Database:', font=(font, 12), justification='left')],
            [sg.Text('Username:', font=(font, 12), justification='left')],
            [sg.Text('Password:', font=(font, 12), justification='left')]
        ]

        col2 = [
            [sg.Input(key='host', font=(font, 12))],
            [sg.Input(key='port', font=(font, 12))],
            [sg.Input(key='database', font=(font, 12))],
            [sg.Input(key='username', font=(font, 12))],
            [sg.InputText('', key='password', password_char='*', font=(font, 12))],
        ]

        layout = [  
            [sg.Text('Query Execution Plan Annotator', key='-text-', font=(font, 20))],
            [sg.Frame(layout=col1, title=''), sg.Frame(layout=col2, title='')],
            [sg.Button('Submit')]
        ]

        # Create the Window
        window = sg.Window('CX4031 Project 2 GUI', layout, element_justification='c').Finalize()
        window.Maximize()
        # Event Loop to process "events" and get the "values" of the inputs
        while True:
            event, values = window.read()
            if event == 'Submit':   # when user clicks submit button
                host = values['host'].lower()   #localhost
                port = values['port']   #5432
                database = values['database']   #whatever ur database name is
                username = values['username'].lower()   #postgres
                password = values['password']   #whatever ur password is

                connect = ConnectAndQuery(host, port, database, username, password)

                if(connect != None):
                    col1 = [
                        [sg.Text('Enter Query:', font=(font, 12), justification='left')]
                    ]

                    col2 = [
                        [sg.Multiline(
                            size=(25, 15),
                            key="query", 
                            font=(font, 12), 
                            autoscroll=True
                        )]
                    ]

                    layout = [
                        [sg.Text("Query Execution Plan Annotator", font=(font, 20))],
                        [sg.Frame(layout=col1, title=''), sg.Frame(layout=col2, title='')],
                        [sg.Button("Submit")]
                    ]

                    query_window = sg.Window('CX4031 Project 2 GUI', layout, element_justification='c').Finalize()
                    window.close()
                    window = query_window
                    window.Maximize()

                    while True:
                        event, values = window.read()
                        if event == "Submit":
                            query = values["query"]

            if event == sg.WIN_CLOSED: # if user closes window
                break

        window.close()

def main():
    app = GUI()
    app.initialise_GUI()

if __name__ == "__main__":
    main()