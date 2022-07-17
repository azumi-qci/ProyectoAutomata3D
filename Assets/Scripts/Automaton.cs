using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automaton
{
    private const int START_STATE = 1;
    private const int END_STATE = 4;

    private int state;

    public Automaton()
    {
        state = START_STATE;
    }

    public bool WalkWithAutomaton(string automatonString)
    {
        foreach (char s in automatonString)
        {
            switch (state)
            {
                case 1:
                    if (s == 'L')
                    {
                        state = 3;
                    }
                    else
                    {
                        state = 2;
                    }
                    break;
                case 2:
                    if (s == 'L')
                    {
                        state = 4;
                    }
                    else
                    {
                        state = 5;
                    }
                    break;
                case 3:
                    if (s == 'L')
                    {
                        state = 5;
                    }
                    else
                    {
                        state = 4;
                    }
                    break;
                case 4:
                    if (s == 'L')
                    {
                        state = 3;
                    }
                    else
                    {
                        state = 2;
                    }
                    break;
                case 5:
                    if (s == 'L')
                    {
                        state = 5;
                    }
                    else
                    {
                        state = 5;
                    }
                    break;
            }
        }

        return state == END_STATE;
    }
}
